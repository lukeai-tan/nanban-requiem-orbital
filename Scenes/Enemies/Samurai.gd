extends CharacterBody2D

class_name Samurai

const Unit = preload("res://Scenes/Unit/Unit.gd")
const HealthBar = preload("res://Scenes/HealthBar/HealthBar.tscn")
var health_bar

var hp : float = 300
@export var movement_speed: float = 100.0
var last_position: Vector2
var block : Unit = null
var attack : float = 100
var attack_speed : float = 0.5
var time_since_last_attack := 0.0

signal despawn()

func _ready():
	position.y -= 25
	scale = Vector2(2, 2)
	var enemy_sprite = get_node("AnimatedSprite2D")
	if enemy_sprite:
		enemy_sprite.play("running")
	
	## Health Bar
	health_bar = HealthBar.instantiate()
	add_child(health_bar)
	health_bar.max_value = hp
	health_bar.value = hp

func _process(delta):
	var path = get_parent()
	time_since_last_attack += delta
	if block != null :
		hit()
	elif path is PathFollow2D :
		path.progress += movement_speed * delta
		
func _get_progress() -> float:
	return get_parent().progress

func get_hit(damage : float) :
	if damage >= hp :
		hp = 0
		despawn.emit()
		get_parent().queue_free()
	else :
		hp = hp - damage
		health_bar.value = hp

func blocked(unit : Unit) :
	block = unit
	unit.despawn.connect(func(): block = null)
	
func hit() :
	if time_since_last_attack >= (1.0 / attack_speed) :
		block.get_hit(attack)
		time_since_last_attack = 0.0
	
