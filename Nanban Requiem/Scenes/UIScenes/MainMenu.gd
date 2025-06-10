extends Control

@onready var default_music = $"Default Music"
@onready var secret_music = $"Secret Music"

func _ready() -> void:
	default_music.playing = true

func _process(_delta: float) -> void:
	pick_music()

func pick_music():
	if(secret_music.playing == false and GameVolume.secret_volume > 0.0):
		print("Playing secret")
		default_music.playing = false
		secret_music.playing = true
	elif(default_music.playing == false and GameVolume.secret_volume == 0.0):
		print("Playing normal")
		secret_music.playing = false
		default_music.playing = true
