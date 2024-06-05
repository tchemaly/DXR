using System;
using OpenAI;
using UnityEngine;
using TMPro;
using UnityEngine.Android;

namespace Samples.Whisper
{
    public class WhisperEdited : MonoBehaviour
    {
        [SerializeField] private TMP_Text message;

        private readonly string fileName = "output.wav";
        private readonly int duration = 4;

        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi("");

        public GameObject whisperobj;
        public GameObject Speakobj;
        public GameObject Transobj;

        public GameObject ScriptManager;

        private void OnEnable()
        {
            isRecording = false;
            time = 0;
            Speakobj.SetActive(true);

            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }

            StartRecording();
        }

        private void StartRecording()
        {
            if (isRecording) return;

            isRecording = true;
            clip = Microphone.Start(null, false, duration, 44100);
        }

        private async void EndRecording()
        {
            Microphone.End(null); // Stop the microphone after recording
            Speakobj.SetActive(false); // Deactivate the Speakobj immediately after recording stops
            Transobj.SetActive(true); 

            message.text = "Transcripting...";

            byte[] data = SaveWav.Save(fileName, clip);

            var req = new CreateAudioTranscriptionsRequest
            {
                FileData = new FileData() { Data = data, Name = "audio.wav" },
                Model = "whisper-1",
                Language = "en"
            };

            var res = await openai.CreateAudioTranscription(req);
            if (!string.IsNullOrEmpty(res.Text))
            {
                message.text = res.Text;
                ScriptManager.GetComponent<bb>().UpdateLabelText(res.Text);
                Transobj.SetActive(false);
            }
            else
            {
                message.text = "Transcription successful but no text returned.";
            }

            this.gameObject.SetActive(false); // Disable this GameObject
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
