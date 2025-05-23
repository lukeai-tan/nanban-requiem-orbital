extends Node2D

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")
const DetectionRange = preload("res://Scenes/DetectionRange/DetectionRange.gd")
const DetectionRangeScene = preload("res://Scenes/DetectionRange/DetectionRange.tscn")

var projectile_scene : PackedScene = load("res://Scenes/Projectile/Projectile.tscn")
var attack : float = 10
var projectile_speed : float = 300
var attack_speed : float = 1
var target : Enemy = null
var attack_range : DetectionRange
var time_since_last_shot := 0.0

func _ready() -> void:
	attack_range = DetectionRangeScene.instantiate()
	add_child(attack_range)

func _process(delta: float) -> void:
	if (target == null or not attack_range.still_in_range()) :
		target = attack_range.find_nearest_enemy()
	elif time_since_last_shot >= (1.0 / attack_speed):
		shoot()
		time_since_last_shot = 0.0

func _physics_process(_delta: float) -> void:
	if (target != null and attack_range.still_in_range()) : turn()

# rotation of turrets on Ranged towers
func turn():
	var enemy_pos = target.global_position
	get_node("Turret").look_at(enemy_pos)

func shoot() :
	var projectile = projectile_scene.instantiate()
	projectile.initialize(attack, projectile_speed, target, global_position)
	get_tree().current_scene.add_child(projectile)
