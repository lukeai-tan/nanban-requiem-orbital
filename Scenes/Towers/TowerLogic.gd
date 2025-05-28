extends Area2D

class_name Tower

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")
const TowerRange = preload("res://Scenes/DetectionRange/TowerRange.gd")
const TowerRangeScene = preload("res://Scenes/DetectionRange/TowerRange.tscn")

const HealthBar = preload("res://Scenes/HealthBar/HealthBar.tscn")
var health_bar

var projectile_scene : PackedScene = load("res://Scenes/Projectile/Projectile.tscn")
var hp : float = 1000
var attack : float = 10
var projectile_speed : float = 300
var attack_speed : float = 3
var target : Enemy = null
var attack_range : TowerRange
var time_since_last_shot := 0.0
var built : bool = false
var build_location


func _ready() -> void:
	if not built:
		return
	attack_range = TowerRangeScene.instantiate()
	add_child(attack_range)
	health_bar = HealthBar.instantiate()
	add_child(health_bar)
	health_bar.max_value = hp
	health_bar.value = hp


func _process(delta: float) -> void:
	if not built:
		return
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
	
signal despawn()


func get_hit(damage : float) :
	if damage >= hp :
		hp = 0
		attack_range.queue_free()
		despawn.emit()
		queue_free()

	else :
		hp = hp - damage
		health_bar.value = hp
