extends "res://Scenes/Towers/Ranged.gd"

func _process(delta):
	super._process(delta)
	var sprite = get_node("AnimatedSprite2D")
	if sprite:
		sprite.play("idle")
