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
        private readonly int duration = 5;

        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi("");

        public GameObject whisperobj;
        public GameObject Speakobj;

        public GameObject ScriptManager;

        private void Start()
        {
            Speakobj.SetActive(true);

            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }

#if UNITY_WEBGL && !UNITY_EDITOR
            dropdown.options.Add(new Dropdown.OptionData("Microphone not supported on WebGL"));
#else
            // foreach (var device in Microphone.devices)
            // {
            //     dropdown.options.Add(new TMP_Dropdown.OptionData(device));
            // }
            // recordButton.onClick.AddListener(StartRecording);
            StartRecording();
            // dropdown.onValueChanged.AddListener(ChangeMicrophone);

            var index = PlayerPrefs.GetInt("user-mic-device-index");
            // dropdown.SetValueWithoutNotify(index);
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

            var res = await openai.CreateAudioTranscription(req);
            if (!string.IsNullOrEmpty(res.Text))
            {
                message.text = res.Text;

                ScriptManager.GetComponent<bb>().UpdateLabelText(res.Text);

                Speakobj.SetActive(false);

            }
            else
            {
                    message.text = "Transcription successful but no text returned.";
            }

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