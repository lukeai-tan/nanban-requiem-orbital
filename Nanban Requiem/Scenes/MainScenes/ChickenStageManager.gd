extends "res://Scenes/MainScenes/GameScene.gd"

func _ready() -> void:
    print("ChickenStageManager is ready")
    tower_manager.change_deployment(8)
    super._ready()