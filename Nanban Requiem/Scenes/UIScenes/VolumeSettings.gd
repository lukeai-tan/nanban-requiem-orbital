extends Control

signal return_to_main_menu

@onready var master_slider = $"VBoxContainer/Master Slider"
@onready var music_slider = $"VBoxContainer/Music Slider"
@onready var sfx_slider = $"VBoxContainer/SFX Slider"


func _ready():
	master_slider.value_changed.connect(_on_MasterSlider_value_changed)
	music_slider.value_changed.connect(_on_MusicSlider_value_changed)
	sfx_slider.value_changed.connect(_on_SFXSlider_value_changed)
	
	master_slider.value = db_to_linear(AudioServer.get_bus_volume_db(AudioServer.get_bus_index("Master")))
	music_slider.value = db_to_linear(AudioServer.get_bus_volume_db(AudioServer.get_bus_index("Music")))
	sfx_slider.value = db_to_linear(AudioServer.get_bus_volume_db(AudioServer.get_bus_index("SFX")))


func _on_MasterSlider_value_changed(value: float) -> void:
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Master"), linear_to_db(value))

func _on_MusicSlider_value_changed(value: float) -> void:
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Music"), linear_to_db(value))

func _on_SFXSlider_value_changed(value: float) -> void:
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("SFX"), linear_to_db(value))



func _on_back_to_main_pressed() -> void:
	emit_signal("return_to_main_menu")
	queue_free()
