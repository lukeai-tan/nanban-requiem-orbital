extends "res://Scenes/Enemies/Samurai.gd"

const EnemyRange = preload("res://Scenes/DetectionRange/EnemyRange.gd")
const EnemyRangeScene = preload("res://Scenes/DetectionRange/EnemyRange.tscn")

var projectile_scene : PackedScene = load("res://Scenes/Projectile/Projectile.tscn")

var ranged_attack_speed : float = 1
var ranged_attack_dmg : float = 100
var projectile_speed : float = 200
var target : Unit = null
var attack_range : EnemyRange


func _ready() -> void:
	attack = 50
	movement_speed = 75
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
	
	target = attack_range.find_nearest_enemy()
	if (block != null) :
		hit()
	elif (target != null) :
		shoot()

func shoot() :
	if time_since_last_attack >= (1.0 / ranged_attack_speed) :
		var projectile = projectile_scene.instantiate()
		projectile.initialize(ranged_attack_dmg, projectile_speed, target, global_position)
		get_tree().current_scene.add_child(projectile)
		time_since_last_attack = 0.0
	
func blocked(unit : Unit) :
	super.blocked(unit)
	target = block
	
