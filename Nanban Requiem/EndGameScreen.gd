extends Control

@onready var background = $Background
@onready var death_message = $"Death Message"
@onready var victory_message = $"Victory Message"

func _ready():
	background.modulate.a = 0.0
	death_message.modulate.a = 0.0
	victory_message.modulate.a = 0.0
	
func _on_game_won():
	fade_in_label(victory_message)

func _on_game_lost():
	fade_in_label(death_message)


func fade_in_label(message: Label):
	var tween = get_tree().create_tween()
	
	tween.tween_property(background, "modulate:a", 1.0, 1.5)\
		.set_trans(Tween.TRANS_SINE).set_ease(Tween.EASE_IN_OUT)

	tween.tween_interval(0.3)
	tween.tween_property(message, "modulate:a", 1.0, 1.5).set_trans(Tween.TRANS_SINE).set_ease(Tween.EASE_IN_OUT)

