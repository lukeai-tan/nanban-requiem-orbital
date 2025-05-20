extends PathFollow2D

@export var speed: float = 100.0
var last_position: Vector2

func _ready():
	last_position = global_position
	var enemy = get_node("Samurai")
	if enemy and enemy.has_node("SamuraiAnimation"):
		enemy.get_node("SamuraiAnimation").play("RESET")

func _process(delta):
	progress += speed * delta
	last_position = global_position
