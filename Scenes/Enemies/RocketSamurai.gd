extends "res://Scenes/Enemies/Samurai.gd"

class_name RocketSamurai
const Tower = preload("res://Scenes/Towers/TowerLogic.gd")
const EnemyRange = preload("res://Scenes/DetectionRange/EnemyRange.gd")
const EnemyRangeScene = preload("res://Scenes/DetectionRange/EnemyRange.tscn")

var projectile_scene : PackedScene = load("res://Scenes/Projectile/Projectile.tscn")
var attack : float = 100
var projectile_speed : float = 200
var attack_speed : float = 1
var target : Tower = null
var attack_range : EnemyRange
var time_since_last_shot := 0.0

func _ready() -> void:
	attack_range = EnemyRangeScene.instantiate()
	add_child(attack_range)

func _process(delta: float) -> void:
	time_since_last_shot += delta
	if (target == null or not attack_range.still_in_range()) :
		target = attack_range.find_nearest_enemy()
	elif time_since_last_shot >= (1.0 / attack_speed):
		shoot()
		time_since_last_shot = 0.0

func shoot() :
	var projectile = projectile_scene.instantiate()
	projectile.initialize(attack, projectile_speed, target, global_position)
	get_tree().current_scene.add_child(projectile)
	
