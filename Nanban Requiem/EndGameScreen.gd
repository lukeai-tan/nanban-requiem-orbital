extends Control

@onready var game_scene 
@onready var background = $"Background"
@onready var death_message = $"Death Message"
@onready var victory_message = $"Victory Message"
@onready var boss_message = $"BossStageMessage"
@onready var return_to_main_button = $"ReturnToMain"
var animation_finished: bool = false
var boss_stage: bool = false

signal game_finished(result)

func _ready():
	visible = false
	background.modulate.a = 0.0
	death_message.modulate.a = 0.0
	victory_message.modulate.a = 0.0
	boss_message.modulate.a = 0.0
	return_to_main_button.visible = false
	
func _on_game_won():
	fade_in_label(victory_message)
	if boss_stage:
		fade_in_label(boss_message)
	print("Game won")

func _on_game_lost():
	fade_in_label(death_message)
	if boss_stage:
		fade_in_label(boss_message)
	print("Game lost")


func fade_in_label(message: Label):
	visible = true
	Engine.set_time_scale(1.0)
	GameData.time_scale = 1.0
	
	var tween = get_tree().create_tween()
	
	tween.tween_property(background, "modulate:a", 1.0, 1.5)\
		.set_trans(Tween.TRANS_SINE).set_ease(Tween.EASE_IN_OUT)

	tween.tween_interval(0.3)
	tween.tween_property(message, "modulate:a", 1.0, 3.0).set_trans(Tween.TRANS_SINE).set_ease(Tween.EASE_IN_OUT)

	tween.tween_callback(Callable(self, "_on_fade_in_complete"))


func _on_fade_in_complete():
	animation_finished = true
	set_mouse_filter(MouseFilter.MOUSE_FILTER_STOP)
	background.mouse_filter = MouseFilter.MOUSE_FILTER_STOP
	return_to_main_button.visible = true


func _on_return_to_main_pressed() -> void:
	print("Button pressed, returning to main menu")
	game_finished.emit("game_finished")
