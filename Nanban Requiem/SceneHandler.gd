extends Node

func _ready():
	load_main_menu()

func load_main_menu():
	if not has_node("MainMenu"):
		var main_menu = load("res://Scenes/UIScenes/MainMenu.tscn").instantiate()
		add_child(main_menu)

	var new_game_button = get_node("MainMenu/Margin/VB_buttons/NewGame")
	if not new_game_button.is_connected("pressed", Callable(self, "on_new_game_pressed")):
		new_game_button.connect("pressed", Callable(self, "on_new_game_pressed"))

	var journal_button = get_node("MainMenu/Margin/VB_buttons/Journal")
	if not journal_button.is_connected("pressed", Callable(self, "on_journal_pressed")):
		journal_button.connect("pressed", Callable(self, "on_journal_pressed"))

	var settings_button = get_node("MainMenu/Margin/VB_buttons/Settings")
	if not settings_button.is_connected("pressed", Callable(self, "on_settings_pressed")):
		settings_button.connect("pressed", Callable(self, "on_settings_pressed"))

	var quit_button = get_node("MainMenu/Margin/VB_buttons/Quit")
	if not quit_button.is_connected("pressed", Callable(self, "on_quit_pressed")):
		quit_button.connect("pressed", Callable(self, "on_quit_pressed"))

func on_new_game_pressed():
	$"MainMenu".queue_free()
	print("New Game button pressed")
	var game_scene: Node2D = load("res://Scenes/MainScenes/GameScene.tscn").instantiate()
	game_scene.connect("game_finished", unload_game)
	call_deferred('add_child', game_scene)
	
'''
func on_trophies_pressed():
	pass
''' 

func on_journal_pressed():
	print("Journal button pressed")
	print("Yes Ziming, the font difference is intended")
	var journal_scene: Control = load("res://Scenes/UIScenes/Journal.tscn").instantiate()
	journal_scene.connect("return_to_main_menu", Callable(self, "load_main_menu"))
	call_deferred('add_child', journal_scene)

func on_settings_pressed():
	print("Settings button pressed")
	var volume_settings_scene: Control = load("res://Scenes/UIScenes/Settings.tscn").instantiate()
	volume_settings_scene.connect("return_to_main_menu", Callable(self, "load_main_menu"))
	call_deferred('add_child', volume_settings_scene)

func on_quit_pressed():
	get_tree().quit()

func unload_game(_result):
	$GameScene.queue_free()
	var main_menu = load("res://Scenes/UIScenes/MainMenu.tscn").instantiate()
	add_child(main_menu)
	load_main_menu()
	
