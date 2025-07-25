extends "res://Scenes/MainScenes/UI.gd"

var state : int = 0
@onready var icon = get_node("HUD/InfoBar/Frame/EnemyIcon")
@onready var overlay = get_node("HUD/InfoBar/HideHPBar")
@onready var priestesshp = get_node("HUD/InfoBar/BossBars/Priestess")
@onready var prtshp = get_node("HUD/InfoBar/BossBars/Prts")
@onready var prtsshieldhp = get_node("HUD/InfoBar/PrtsShield")

func corrode():
	base_hp_bar.value -= 1
	update_color()

func update_ui():
	if state == 0:
		state = 1
		icon.texture = preload("res://Assets/Enemies/REDACTED.png")
		overlay.position.y -= 50
	elif state == 1:
		state = 0
		icon.texture = preload("res://Assets/Enemies/PRTS.png")
		overlay.position.y += 50

func get_priestess_healthbar():
	return priestesshp

func get_prts_healthbar():
	return prtshp

func get_prts_shieldbar():
	return prtsshieldhp

