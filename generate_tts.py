import sys

from TTS.api import TTS

def generate(text, output_file="output.wav"):
        tts = TTS(model_name="tts_models/ja/kokoro/tacotron2-DDC", progress_bar=False)
        tts.tts_to_file(text=text, file_path=output_file)

        return output_file

if __name__ == "__main__":
    if len(sys.argv) < 2:
        sys.exit(1)

    message = sys.argv[1]

    output_file = "output.wav"
    if len (sys.argv) > 2:
        output_file = sys.argv[2]

    result = generate(message, output_file)