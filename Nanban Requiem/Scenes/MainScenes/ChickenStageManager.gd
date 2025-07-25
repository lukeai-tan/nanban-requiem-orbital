extends "res://Scenes/MainScenes/GameScene.gd"

func _ready() -> void:
	print("ChickenStageManager is ready")
	tower_manager.change_deployment(8)
	super._ready()

func _on_wave_complete():
	if(game_state != "defeat"):
		game_won.emit()
		GameData.chicken_don_dead = true
