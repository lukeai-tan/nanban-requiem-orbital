extends Area2D

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")

var targets_in_range : Array[Enemy] = []
var curr_target : Enemy

func _on_enemy_entered(enemy: Enemy) -> void:
	targets_in_range.append(enemy)
	enemy.despawn.connect(func(): _on_enemy_exited(enemy))

func _on_enemy_exited(enemy: Enemy) -> void:
	if enemy == curr_target :
		curr_target = null
	else :
		targets_in_range.remove_at(targets_in_range.find(enemy))

func find_nearest_enemy() -> Enemy:
	targets_in_range.sort_custom(func(enemy1, enemy2) : return (global_position - enemy1.global_position).length < (global_position - enemy2.global_position).length)
	if targets_in_range.is_empty() : 
		curr_target = null
	else :
		curr_target = targets_in_range.pop_front()
	return curr_target
		
func still_in_range() -> bool:
	return curr_target != null
