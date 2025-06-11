extends Node



'''
# Template for Tower Data
"Tower name": {
		"range": Tower range,
		"sprite_icon": "res://filepath",
		"sprite_in_game": "res://filepath",
		"name": "Tower name",
		"description": "Tower description",
		"stats": "Tower stats"
	},
'''






var tower_data = {
	"RangedTower1": {
		"range": 300,
		"sprite_icon": "res://Assets/Towers/RangedTower1_turret.png",
		"sprite_in_game": "res://Assets/Towers/RangedTower1_turret.png",
		"name": "Pew Pew",
		"description": "Pew Pew is the first prototype of the engineers' ranged towers.\n 
						It shoots fast and accurate rockets that deal moderate damage to enemies.",
		"stats": ""
	},
		
	"RangedTower2": {
		"range": 600,
		"sprite_icon": "res://Assets/Towers/RangedTower2_turret.png",
		"sprite_in_game": "res://Assets/Towers/RangedTower2_turret.png",
		"name": "Rocketeer",
		"description": "Born from the Pew Pew prototype, the Rocketeer is the engineers' answer to heavy firepower.\n 
						The rocketeer shoots explosive rockets that deal devasting area damage, blasting enemies into dust with each slow but powerful shot.",
		"stats": "Rocketeer stats"
	},
	
	"Obstacle1": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/Obstacle1_base.png",
		"sprite_in_game": "res://Assets/Towers/Obstacle1_base.png",
		"name": "Boulder",
		"description": "Serves as a deployable obstacle or a meatshield to hinder enemy movement.\nIt is also unable to attack enemies.\n\nRespect the Boulder.",
		"stats": "Boulder stats"
	},
		
	"MeleeTower1": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/MeleeTower1_base.png",
		"sprite_in_game": "res://Assets/Towers/MeleeTower1_base.png",
		"name": "Inverse Boulder",
		"description": "An exact replica of the Boulder, but is able to attack blocked enemies.",
		"stats": ""
	},
	
	"RangedTowerGirl1": {
		"range": 600,
		"sprite_icon": "res://Assets/Towers/RangedTowerGirl1_icon.png",
		"sprite_in_game": "res://Assets/Towers/RangedTowerGirl1_base.png",
		"name": "Ranged Tower Girl",
		"description": "Unleashes fiery blasts that deal powerful area damage. Where she stands, enemy waves burn and fall.",
		"stats": "Girl stats"
	},


	"RangedTowerGirl2": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/RangedTowerGirl2_icon.png",
		"sprite_in_game": "res://Assets/Towers/RangedTowerGirl2_base.png",
		"name": "Nyx",
		"description": "Silent and swift, Nyx uses blood magic to sap strength from enemies.",
		"stats": "Vampire girl stats"
	},


	"RangedTowerBoy1": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/RangedTowerBoy1_icon.png",
		"sprite_in_game": "res://Assets/Icons/coming_soon_icon.png",
		"name": "Kakyoin Ripoff",
		"description": "Kakyoin description",
		"stats": "Kakyoin stats"
	},

	"RangedTowerBoy2": {
		"range": 0,
		"sprite_icon": "res://Assets/Icons/coming_soon_icon.png",
		"sprite_in_game": "res://Assets/Icons/coming_soon_icon.png",
		"name": "Dio Brando Ripoff",
		"description": "Dio Brando Ripoff description",
		"stats": "Dio Brando stats"
	},

	"Chicken Don": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/ChickenDon_icon.png",
		"sprite_in_game": "res://Assets/Towers/ChickenDon_sprite.png",
		"name": "Chicken 'Don'",
		"description": "The Chicken, The Myth, The Legend.\n
						This is the 'Before' image of the Spicy Tartar Chicken Nanban Don",
		"stats": "Chicken Don stats"
	},

}


var enemy_data = {
	"Samurai": {
		"health": 100,
		"speed": 50,
		"sprite_icon": "res://Assets/Enemies/samurai_sprite.png",
		"sprite_in_game": "res://Assets/Enemies/samurai_sprite.png",
		"name": "Samurai",
		"description": "This is a Samurai description",
		"stats": "Samurai stats"
	},
	
	"RocketSamurai": {
		"health": 200,
		"speed": 75,
		"sprite_icon": "res://Assets/Icons/coming_soon_icon.png",
		"sprite_in_game": "res://Assets/Icons/coming_soon_icon.png",
		"name": "Rocket Samurai",
		"description": "This is a Rocket Samurai description",
		"stats": "Rocket Samurai stats"
	},
	
	"WhiteSamurai": {
		"health": 300,
		"speed": 100,
		"sprite_icon": "res://Assets/Icons/coming_soon_icon.png",
		"sprite_in_game": "res://Assets/Icons/coming_soon_icon.png",
		"name": "Mighty Whitey",
		"description": "This is a Mighty Whitey description",
		"stats": "Mighty Whitey stats"
	}
}
	

var wave_data = {
	"Map1": [
		# Wave 1
		["Samurai", 0.0],
		
		# Wave 2
		["Samurai", 5.0],
		["RocketSamurai", 2.0],
		
		# Wave 3
		["WhiteSamurai", 10.0],
		
		# Wave 4
		["Samurai", 2.0],
		["Samurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		
		# Wave 5
		["WhiteSamurai", 6.0],
		["WhiteSamurai", 2.0],
		
		# Wave 6
		["Samurai", 5.0],
		["Samurai", 1.0],
		["Samurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["WhiteSamurai", 2.0],
		["WhiteSamurai", 2.0],
		["WhiteSamurai", 1.0],

		# Death
		["Samurai", 0.0],
		["Samurai", 1.0],
		["Samurai", 0.0],
		["Samurai", 0.0],
		["Samurai", 1.0],
		["Samurai", 0.0],
		["Samurai", 0.0],
		["Samurai", 1.0],
		["Samurai", 1.0],
		["Samurai", 0.0],
		["Samurai", 0.0],
		["Samurai", 1.0],
		["Samurai", 1.0],
		["Samurai", 1.0],
		["Samurai", 1.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],

		
	],
	
	"Samurai": [
		["Samurai", 0.0],
		["Samurai", 0.0],
		["Samurai", 0.0],
		["Samurai", 0.0],
		["Samurai", 0.0],
	],

	"RocketSamurai": [
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
	],

	"WhiteSamurai": [
		["WhiteSamurai", 0.0],
		["WhiteSamurai", 0.0],
		["WhiteSamurai", 0.0],
		["WhiteSamurai", 1.0],
		["WhiteSamurai", 1.0],
	],
}
