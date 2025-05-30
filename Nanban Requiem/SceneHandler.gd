extends Node

func _ready():
	load_main_menu()

func load_main_menu():
	get_node("MainMenu/Margin/VB_buttons/NewGame").connect("pressed", Callable(self, "on_new_game_pressed"))
	# get_node("MainMenu/Margin/VB_buttons/Trophies").connect("pressed", Callable(self, "on_trophies_pressed"))
	get_node("MainMenu/Margin/VB_buttons/Settings").connect("pressed", Callable(self, "on_settings_pressed"))
	get_node("MainMenu/Margin/VB_buttons/Credits").connect("pressed", Callable(self, "on_credits_pressed"))
	get_node("MainMenu/Margin/VB_buttons/Quit").connect("pressed", Callable(self, "on_quit_pressed"))

func on_new_game_pressed():
	'''
	get_tree().change_scene_to_file("res://Scenes/MainScenes/GameScene.tscn")
	get_tree().connect("game_finished", Callable(self, "unload_game"))
	'''
	$"MainMenu".queue_free()
	var game_scene: Node2D = load("res://Scenes/MainScenes/GameScene.tscn").instantiate()
	game_scene.connect("game_finished", unload_game)
	call_deferred('add_child', game_scene)
	
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

func unload_game(result):
	$GameScene.queue_free()
	var main_menu = load("res://Scenes/UIScenes/MainMenu.tscn").instantiate()
	add_child(main_menu)
	load_main_menu()
	
