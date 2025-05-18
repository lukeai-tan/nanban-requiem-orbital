extends Node

func _ready():
	get_node("MainMenu/Margin/VB_buttons/NewGame").connect("pressed", Callable(self, "onNewGamePressed"))
	# get_node("MainMenu/Margin/VB_buttons/Trophies").connect("pressed", Callable(self, "onTrophiesPressed"))
	get_node("MainMenu/Margin/VB_buttons/Settings").connect("pressed", Callable(self, "onSettingsPressed"))
	get_node("MainMenu/Margin/VB_buttons/Credits").connect("pressed", Callable(self, "onCreditsPressed"))
	get_node("MainMenu/Margin/VB_buttons/Quit").connect("pressed", Callable(self, "onQuitPressed"))

func onNewGamePressed():
	get_node("MainMenu").queue_free()
	var gameScene = load("res://Scenes/MainScenes/GameScene.tscn").instantiate()
	add_child(gameScene)
	
'''
func onTrophiesPressed():
	pass
''' 
func onSettingsPressed():
	pass
	
func onCreditsPressed():
	pass

func onQuitPressed():
	get_tree().quit()
