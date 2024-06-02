using System;
using OpenAI;
using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

namespace Samples.Whisper
{
    public class WhisperEdited : MonoBehaviour
    {
        [SerializeField] private TMP_Text message; // Change to TMP_Text

        private readonly string fileName = "output.wav";
        private readonly int duration = 5;

        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi();

        public bb bbScript;

        private void Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            message.text = "Microphone not supported on WebGL";
#else
            if (Microphone.devices.Length > 0)
            {
                StartRecording();
            }
            else
            {
                message.text = "No microphone detected";
            }
#endif
        }

        private void StartRecording()
        {
            isRecording = true;
            #if !UNITY_WEBGL
                        // Start recording from the default microphone
                        clip = Microphone.Start(null, false, duration, 44100);
            #endif
        }

        private async void EndRecording()
        {
            message.text = "Transcripting...";

            byte[] data = SaveWav.Save(fileName, clip);

            var req = new CreateAudioTranscriptionsRequest
            {
                FileData = new FileData() { Data = data, Name = "audio.wav" },
                Model = "whisper-1",
                Language = "en"
            };
            try
            {
                var res = await openai.CreateAudioTranscription(req);
                if (!string.IsNullOrEmpty(res.Text))
                {
                    message.text = res.Text;
                }
                else
                {
                    message.text = "Transcription successful but no text returned.";
                }
            }
            catch (Exception e)
            {
                message.text = "Error: " + e.Message;
            }



            //if (bbScript.CurrentCube != null)
            //{
            //    Transform textTransform = bbScript.CurrentCube.transform.Find("Text");
            //    TMP_Text childTextComponent = textTransform.GetComponent<TMP_Text>();
            //    childTextComponent.text = res.Text;
            //}
        }

        private void Update()
        {
            if (isRecording)
            {
                time += Time.deltaTime;

                if (time >= duration)
                {
                    time = 0;
                    isRecording = false;
                    EndRecording();
                }
            }
        }
    }
}
