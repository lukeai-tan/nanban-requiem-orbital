extends "res://Scenes/Unit/Unit.gd"

class_name Ranged

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")
const TowerRange = preload("res://Scenes/DetectionRange/TowerRange.gd")
const TowerRangeScene = preload("res://Scenes/DetectionRange/TowerRange.tscn")

var projectile_scene : PackedScene = load("res://Scenes/Projectile/Projectile.tscn")
var attack : float = 10
var projectile_speed : float = 300
var attack_speed : float = 3
var target : Enemy = null
var attack_range : TowerRange
var time_since_last_shot := 0.0

func _ready() -> void:
	super._ready()
	attack_range = TowerRangeScene.instantiate()
	add_child(attack_range)

func _process(delta: float) -> void:
	time_since_last_shot += delta
	target = attack_range.find_nearest_enemy()
	if (target != null and time_since_last_shot >= (1.0 / attack_speed)):
		shoot()
		time_since_last_shot = 0.0

func _physics_process(_delta: float) -> void:
	if (target != null) : turn()

# rotation of turrets on Ranged towers
func turn():
	var enemy_pos = target.global_position
	get_node("Turret").look_at(enemy_pos)

func shoot() :
	var projectile = projectile_scene.instantiate()
	projectile.initialize(attack, projectile_speed, target, global_position)
	get_tree().current_scene.add_child(projectile)

func get_hit(damage : float) :
	if damage >= hp :
		attack_range.queue_free()
	super.get_hit(damage)
