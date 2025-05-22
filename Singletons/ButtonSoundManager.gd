extends Node

@export var hover_sound: AudioStream
@export var press_sound: AudioStream

var player := AudioStreamPlayer.new()

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	add_child(player)
	hover_sound = load("res://Assets/UI/Sounds/click-b.ogg")
	press_sound = load("res://Assets/UI/Sounds/tap-a.ogg")

func play_hover_sound():
	player.stream = hover_sound
	player.play()

func play_press_sound():
	player.stream = press_sound
	player.play()
