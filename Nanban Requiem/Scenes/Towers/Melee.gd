extends "res://Scenes/Unit/Unit.gd"

class_name Melee

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")

var attack : float = 30
var block_count : int = 2
var current_block_count : int = 0
var enemies_blocked : Array[Enemy] = []
var attack_speed : float = 1
var target : Enemy = null
var time_since_last_hit := 0.0

func _ready() -> void:
	hp = 1000
	super._ready()
	
func _process(delta: float) -> void:
	time_since_last_hit += delta
	if (target != null and time_since_last_hit >= (1.0 / attack_speed)):
		hit()
		time_since_last_hit = 0.0

func _on_enemy_entered(enemy: Enemy) -> void:
	if current_block_count < block_count :
		enemy.blocked(self)
		enemies_blocked.append(enemy)
		enemy.despawn.connect(func(): _on_enemy_exited(enemy))
		current_block_count += 1
		target = get_next_target()

func _on_enemy_exited(enemy: Enemy) -> void:
	var idx = enemies_blocked.find(enemy)
	if idx != -1:
		enemies_blocked.remove_at(idx)
	current_block_count -= 1
	target = get_next_target()

func hit():
	pass
	
func get_next_target() -> Enemy:
	return null
