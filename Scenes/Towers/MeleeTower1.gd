extends "res://Scenes/Unit/Unit.gd"

class_name Obstacle

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")

var block_count : int = 2
var current_block_count : int = 0
var enemies_blocked : Array[Enemy] = []

func _ready() -> void:
	built = true
	super._ready()

func _on_enemy_entered(enemy: Enemy) -> void:
	print("Melee Tower: hi")
	if current_block_count < block_count :
		enemy.blocked(self)
		enemies_blocked.append(enemy)
		enemy.despawn.connect(func(): _on_enemy_exited(enemy))
		current_block_count += 1

func _on_enemy_exited(enemy: Enemy) -> void:
	print("Melee Tower: bye")
	var idx = enemies_blocked.find(enemy)
	if idx != -1:
		enemies_blocked.remove_at(idx)
	current_block_count -= 1
