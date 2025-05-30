extends Node

func _ready():
	get_node("MainMenu/Margin/VB_buttons/NewGame").connect("pressed", Callable(self, "on_new_game_pressed"))
	# get_node("MainMenu/Margin/VB_buttons/Trophies").connect("pressed", Callable(self, "on_trophies_pressed"))
	get_node("MainMenu/Margin/VB_buttons/Settings").connect("pressed", Callable(self, "on_settings_pressed"))
	get_node("MainMenu/Margin/VB_buttons/Credits").connect("pressed", Callable(self, "on_credits_pressed"))
	get_node("MainMenu/Margin/VB_buttons/Quit").connect("pressed", Callable(self, "on_quit_pressed"))

func on_new_game_pressed():
	'''
	get_node("MainMenu").queue_free()
	var gameScene = load("res://Scenes/MainScenes/GameScene.tscn").instantiate()
	add_child(gameScene)
	'''
	get_tree().change_scene_to_file("res://Scenes/MainScenes/GameScene.tscn")
	
'''
func on_trophies_pressed():
	pass
''' 
func on_settings_pressed():
	pass
	
func on_credits_pressed():
	pass

func on_quit_pressed():
	get_tree().quit()

func unload_game():
	get_node("GameScene").queue_free()
	get_tree().change_scene_to_file("res://Scenes/MainScenes/GameScene.tscn")
