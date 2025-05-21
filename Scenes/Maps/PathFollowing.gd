extends PathFollow2D

@export var speed: float = 500.0
var last_position: Vector2

func _ready():
	last_position = global_position
	var enemy_sprite = get_node("Samurai/AnimatedSprite2D")
	if enemy_sprite:
		enemy_sprite.play("running")

func _process(delta):
	progress += speed * delta
	last_position = global_position
