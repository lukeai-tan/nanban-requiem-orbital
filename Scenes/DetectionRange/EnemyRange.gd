extends "res://Scenes/DetectionRange/DetectionRange.gd"

const Tower = preload("res://Scenes/Towers/TowerLogic.gd")

func _on_enemy_entered(enemy: Tower):
	_on_body_entered(enemy)
	
func _on_enemy_exited(enemy: Tower):
	_on_body_exited(enemy)

func _distance_sort(enemy1: Tower, enemy2: Tower) -> bool:
	return (global_position - enemy1.global_position).length() < (global_position - enemy2.global_position).length()

func sort_by_priority() -> void:
	targets_in_range.sort_custom(Callable(self, "_distance_sort"))

func find_nearest_enemy() -> Tower:
	return find_nearest_body() as Tower
