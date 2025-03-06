Use Python3.11 not newer not my fault Coqui-TTS needs 3.11

Setup a python venv with `python3.11 -m venv venv`

Activate the venv Bash`source venv/bin/activate`, Fish `source venv/bin/activate.fish`

If using CUDA for TTS install `pip install torch==2.3.0 torchvision==0.18.0 torchaudio==2.3.0 --index-url https://download.pytorch.org/whl/cu118`

If using CPU for TTS install `pip install torch==2.3.0 torchvision==0.18.0 torchaudio==2.3.0 --index-url https://download.pytorch.org/whl/cpu`

