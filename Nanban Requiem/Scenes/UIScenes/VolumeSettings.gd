extends Control

signal return_to_main_menu

@onready var master_slider = $"VBoxContainer/Master Slider"
@onready var music_slider = $"VBoxContainer/Music Slider"
@onready var sfx_slider = $"VBoxContainer/SFX Slider"
@onready var secret_label = $"VBoxContainer/Secret"
@onready var secret_slider = $"VBoxContainer/Secret Slider"


func _ready():
	master_slider.value_changed.connect(_on_MasterSlider_value_changed)
	music_slider.value_changed.connect(_on_MusicSlider_value_changed)
	sfx_slider.value_changed.connect(_on_SFXSlider_value_changed)
	secret_slider.value_changed.connect(_on_SecretSlider_value_changed)
	
	secret_slider_visibility()
	secret_slider.value = GameVolume.secret_volume
	
	master_slider.value = db_to_linear(AudioServer.get_bus_volume_db(AudioServer.get_bus_index("Master")))
	music_slider.value = db_to_linear(AudioServer.get_bus_volume_db(AudioServer.get_bus_index("Music")))
	sfx_slider.value = db_to_linear(AudioServer.get_bus_volume_db(AudioServer.get_bus_index("SFX")))
	secret_slider.value = db_to_linear(AudioServer.get_bus_volume_db(AudioServer.get_bus_index("Secret")))


func _process(_delta):
	secret_slider_visibility()

func _on_MasterSlider_value_changed(value: float) -> void:
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Master"), linear_to_db(value))

func _on_MusicSlider_value_changed(value: float) -> void:
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Music"), linear_to_db(value))

func _on_SFXSlider_value_changed(value: float) -> void:
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("SFX"), linear_to_db(value))
	
func _on_SecretSlider_value_changed(value: float) -> void:
	GameVolume.secret_volume = value
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Secret"), linear_to_db(value))

func secret_slider_visibility():
	if(master_slider.value == 0 and music_slider.value == 0 and sfx_slider.value == 0):
		secret_label.visible = true
		secret_slider.visible = true
	else:
		secret_label.visible = false
		secret_slider.visible = false

func _on_back_to_main_pressed() -> void:
	emit_signal("return_to_main_menu")
	queue_free()
