extends "res://Scenes/Enemies/Samurai.gd"

class_name RocketSamurai
const EnemyRange = preload("res://Scenes/DetectionRange/EnemyRange.gd")
const EnemyRangeScene = preload("res://Scenes/DetectionRange/EnemyRange.tscn")

var projectile_scene : PackedScene = load("res://Scenes/Projectile/Projectile.tscn")
var attack : float = 100
var projectile_speed : float = 200
var attack_speed : float = 1
var target : Unit = null
var attack_range : EnemyRange
var time_since_last_shot := 0.0

func _ready() -> void:
	movement_speed = 200
	attack_range = EnemyRangeScene.instantiate()
	add_child(attack_range)
	
	## Health Bar 
	health_bar = HealthBar.instantiate()
	add_child(health_bar)
	health_bar.max_value = hp
	health_bar.value = hp
	
	position.y -= 25
	scale = Vector2(2, 2)
	var enemy_sprite = get_node("AnimatedSprite2D")
	if enemy_sprite:
		enemy_sprite.play("running")


func _process(delta: float) -> void:
	### Added this
	super._process(delta)
	attack_range.global_position = global_position
	###
	
	time_since_last_shot += delta
	if block == null : target = attack_range.find_nearest_enemy()
	if (target != null and time_since_last_shot >= (1.0 / attack_speed)):
		shoot()
		time_since_last_shot = 0.0

func shoot() :
	var projectile = projectile_scene.instantiate()
	projectile.initialize(attack, projectile_speed, target, global_position)
	get_tree().current_scene.add_child(projectile)
	
func blocked(unit : Unit) :
	super.blocked(unit)
	target = block
	
