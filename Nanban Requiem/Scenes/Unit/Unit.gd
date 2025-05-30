extends Area2D

class_name Unit

const HealthBar = preload("res://Scenes/HealthBar/HealthBar.tscn")
var health_bar

var hp : float = 1000
var built : bool = false
var build_location


func _ready() -> void:
	if not built:
		return
	health_bar = HealthBar.instantiate()
	add_child(health_bar)
	health_bar.max_value = hp
	health_bar.value = hp


func _process(delta: float) -> void:
	return
	
func _physics_process(_delta: float) -> void:
	return

signal despawn()

func get_hit(damage : float) :
	if damage >= hp :
		hp = 0
		despawn.emit()
		queue_free()

	else :
		hp = hp - damage
		health_bar.value = hp
