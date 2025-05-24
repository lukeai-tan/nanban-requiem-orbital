extends "res://Scenes/DetectionRange/DetectionRange.gd"

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")

func _on_enemy_entered(enemy: Enemy):
	_on_body_entered(enemy)
	
func _on_enemy_exited(enemy: Enemy):
	_on_body_exited(enemy)

func sort_by_priority() -> void:
	targets_in_range.sort_custom(func(enemy1, enemy2) : return (global_position - enemy1.global_position).length < (global_position - enemy2.global_position).length)

func find_nearest_enemy() -> Enemy:
	return find_nearest_body() as Enemy
