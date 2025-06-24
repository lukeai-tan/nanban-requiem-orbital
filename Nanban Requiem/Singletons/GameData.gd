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

var tower_squad = {
	"RangedTower1": true,
	"RangedTower2": true,
	"Obstacle1": true,
	"MeleeTower1": true,
	"RangedTowerGirl1": true,
	"RangedTowerGirl2": true,
	"RangedTowerBoy1": false,
	"RangedTowerBoy2": false,
	"Chicken Don": false,
}



var tower_data = {
	"RangedTower1": {
		"range": 300,
		"sprite_icon": "res://Assets/Towers/RangedTower1_turret.png",
		"sprite_in_game": "res://Assets/Towers/RangedTower1_turret.png",
		"name": "Pew Pew",
		"description": "Pew Pew is the first prototype of the engineers' ranged towers.\n 
						It shoots fast and accurate rockets that deal moderate damage to enemies.",
		"stats": "Health: 1000\nRanged DMG: 15\nASPD: 3"
	},
		
	"RangedTower2": {
		"range": 600,
		"sprite_icon": "res://Assets/Towers/RangedTower2_turret.png",
		"sprite_in_game": "res://Assets/Towers/RangedTower2_turret.png",
		"name": "Rocketeer",
		"description": "Born from the Pew Pew prototype, the Rocketeer is the engineers' answer to heavy firepower.\n 
						The rocketeer shoots explosive rockets that deal devasting area damage, blasting enemies into dust with each slow but powerful shot.",
		"stats": "Health: 800\nRanged DMG: 30\nASPD: 0.7"
	},
	
	"Obstacle1": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/Obstacle1_base.png",
		"sprite_in_game": "res://Assets/Towers/Obstacle1_base.png",
		"name": "Boulder",
		"description": "Serves as a deployable obstacle or a meatshield to hinder enemy movement.\nIt is also unable to attack enemies.\n\nRespect the Boulder.",
		"stats": "Health: 1500\nMelee DMG: 0\nBlock Count: 3"
	},
		
	"MeleeTower1": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/MeleeTower1_base.png",
		"sprite_in_game": "res://Assets/Towers/MeleeTower1_base.png",
		"name": "Inverse Boulder",
		"description": "An exact replica of the Boulder, but is able to attack blocked enemies.",
		"stats": "Health: 1000\nMelee DMG : 30\nBlock Count: 2\nASPD: 1.0"
	},
	
	"RangedTowerGirl1": {
		"range": 600,
		"sprite_icon": "res://Assets/Towers/RangedTowerGirl1_icon.png",
		"sprite_in_game": "res://Assets/Towers/RangedTowerGirl1_base.png",
		"name": "Ranged Tower Girl",
		"description": "Unleashes gravity magic on enemies, slowing them down while dealing damage.",
		"stats": "Health: 1000\nRanged DMG: 30\nASPD: 0.5\nDebuff: Slow"
	},


	"RangedTowerGirl2": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/RangedTowerGirl2_icon.png",
		"sprite_in_game": "res://Assets/Towers/RangedTowerGirl2_base.png",
		"name": "Nyx",
		"description": "Silent and swift, Nyx uses blood magic on enemies to inflict damage over time.\n
						Her special ability unleashes a blood-soaked area-of-effect attack, 
						absorbing the life essence of all nearby foes to heal herself.\n",
		"stats": "Health: 500\nRanged DMG: 100\nASPD: 0.3\nDebuff: Bleed"
	},


	"RangedTowerBoy1": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/RangedTowerBoy1_icon.png",
		"sprite_in_game": "res://Assets/Icons/coming_soon_icon.png",
		"name": "Kekyoin",
		"description": "Calm and precise, Kekyoin fights from a distance with uncanny control.\nHis attacks arc through the battlefield like threads weaving fate.",
		"stats": "Coming soon"
	},

	"RangedTowerBoy2": {
		"range": 0,
		"sprite_icon": "res://Assets/Icons/coming_soon_icon.png",
		"sprite_in_game": "res://Assets/Icons/coming_soon_icon.png",
		"name": "Diego",
		"description": "Charismatic and commanding, Diego bends others to his will with sheer presence.\nTime seems to favor him - his strikes land just when it matters most.",
		"stats": "Coming soon"
	},

	"Chicken Don": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/ChickenDon_icon.png",
		"sprite_in_game": "res://Assets/Towers/ChickenDon_sprite.png",
		"name": "Chicken 'Don'",
		"description": "The Chicken, The Myth, The Legend.\n
						This is the 'Before' image of the Spicy Tartar Chicken Nanban Don",
		"stats": "Chicken Don does not reveal his stats. He lets his muscles do the talking."
	},

}


var enemy_data = {
	"Samurai": {
		"health": 500,
		"speed": 100,
		"sprite_icon": "res://Assets/Enemies/samurai_sprite.png",
		"sprite_in_game": "res://Assets/Enemies/samurai_sprite.png",
		"name": "Samurai",
		"description": "He is the first enemy you encounter in the game.\n",
		"stats": "Health: 500\nSpeed: 100\nMelee DMG : 100"
	},
	
	"RocketSamurai": {
		"health": 300,
		"speed": 100,
		"sprite_icon": "res://Assets/Enemies/RocketSamurai_sprite.png",
		"sprite_in_game": "res://Assets/Enemies/RocketSamurai_sprite.png",
		"name": "Rocket Samurai",
		"description": "After getting hold of Pew Pew's technology, the Samurai upgraded his arsenal with a rocket launcher.\n
						He is able to shoot rockets that deal damage to enemies.",
		"stats": "Health: 300\nSpeed: 100\nMelee DMG: 50\nRanged DMG: 100"
	},
	
	"WhiteSamurai": {
		"health": 300,
		"speed": 100,
		"sprite_icon": "res://Assets/Enemies/WhiteSamurai_sprite.png",
		"sprite_in_game": "res://Assets/Enemies/WhiteSamurai_sprite.png",
		"name": "Mighty Whitey",
		"description": "Mighty Whitey is the Leader of the Samurai Clan. He embodies the common white saviour stereotype in western media and uses it to his advantage.\n",
		"stats": "Health: 200\nSpeed: 200\nMelee DMG : 150"
	},

	"Unknown": {
		"health": 0,
		"speed": 0,
		"sprite_icon": "res://Assets/Enemies/REDACTED.png",
		"sprite_in_game": "res://Assets/Enemies/PRTS.png",
		"name": "XXXX",
		"description": "I'm not upset",
		"stats": "[REDACTED]"
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
		["WhiteSamurai", 2.0],
		["WhiteSamurai", 2.0],


	],

	"Map2": [
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
		["WhiteSamurai", 2.0],
		["WhiteSamurai", 2.0],

	],


	"Map3": [
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
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],
		["RocketSamurai", 0.0],


	]
	
}
