extends Node

func _ready():
	get_node("MainMenu/Margin/VBoxContainer/NewGame").connect("pressed", Callable(self, "onNewGamePressed"))
	get_node("MainMenu/Margin/VBoxContainer/Trophies").connect("pressed", Callable(self, "onTrophiesPressed"))
	get_node("MainMenu/Margin/VBoxContainer/Settings").connect("pressed", Callable(self, "onSettingsPressed"))
	get_node("MainMenu/Margin/VBoxContainer/Credits").connect("pressed", Callable(self, "onCreditsPressed"))
	get_node("MainMenu/Margin/VBoxContainer/Quit").connect("pressed", Callable(self, "onQuitPressed"))

func onNewGamePressed():
	get_node("MainMenu").queue_free()
	# var gameScene = load("res://Scenes/MainScenes/GameScene.tscn").instance()
	# add_child(gameScene)
	
func onTrophiesPressed():
	pass
	
func onSettingsPressed():
	pass
	
func onCreditsPressed():
	pass

func onQuitPressed():
	get_tree().quit()
