extends Control

@onready var label = $"Label"

func _ready():
    label.text = "Insert text"
    visible = false
    modulate.a = 1.0
    label.modulate.a = 1.0
    label.self_modulate = Color.WHITE

func black_title_card():
    label.self_modulate = Color(0, 0, 0)

func white_title_card():
    label.self_modulate = Color(1, 1, 1)

func hide_title_card():
    visible = false

func activate_title_sequence(text: String):
    label.text = text
    visible = true
    label.modulate.a = 0.0

    var tween = get_tree().create_tween()

    tween.tween_property(label, "modulate:a", 1.0, 1.0).set_trans(Tween.TRANS_SINE).set_ease(Tween.EASE_IN_OUT)
    tween.tween_interval(1.0)
    tween.tween_property(label, "modulate:a", 0.0, 1.0).set_trans(Tween.TRANS_SINE).set_ease(Tween.EASE_IN_OUT)
    tween.tween_callback(Callable(self, "hide_title_card"))