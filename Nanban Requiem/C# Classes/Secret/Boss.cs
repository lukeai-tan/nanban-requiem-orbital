using System;
using System.Collections.Generic;
using System.Linq;
using Godot;


public abstract partial class Boss : Enemy, IAct
{

    protected bool invulnerable = true;
    protected bool incapacitated = false;
    protected double timeSinceLastSkill = 0;
    [Export] protected double skillcooldown;
    protected List<BossSkill> skills;
    protected GlobalDetectionRange range;
    public event EventHandler HasEnemy;
    public event EventHandler HasTower;

    public override void _Ready()
    {
        base._Ready();
        this.range = this.GetNodeOrNull<GlobalDetectionRange>("Range");
    }

    public abstract void SetActions();

    protected override void TakeDamage(int damage)
    {
        if (!this.invulnerable)
        {
            int trueDamage = damage > 1 ? damage : 1;
            this.health -= trueDamage;
            this.healthBar.Value = this.health;
            double ratio = (double)this.health / (double)this.maxHealth;
            switch (ratio)
            {
                case <= 0:
                    this.ZeroF();
                    break;
                case <= 0.25:
                    this.OneQF();
                    break;
                case <= 0.5:
                    this.HalfF();
                    break;
                case <= 0.75:
                    this.ThreeQF();
                    break;
            }
        }
    }

    protected abstract void ZeroF();

    protected abstract void OneQF();

    protected abstract void HalfF();

    protected abstract void ThreeQF();

    public abstract void Act();

    protected void CheckTargets()
    {
        if (this.range.GetAllEnemies().Count > 0)
        {
            this.HasEnemy?.Invoke(this, EventArgs.Empty);
        }
        if (this.range.GetAllTowers().Count > 0)
        {
            this.HasTower?.Invoke(this, EventArgs.Empty);
        }
    }

    protected BossSkill ChooseSkill()
    {
        List<BossSkill> usable = this.skills.Where(skill => skill.IsUsable()).ToList();
        usable.Sort((e1, e2) => e1.GetPriority().CompareTo(e2.GetPriority()));
        return usable.LastOrDefault();
    }

    public virtual void Initialize()
    {
        this.pathing = new BossPathing(this);
    }

    public override void ModifyMovementSpeed(double multiplier) { }

    public override void ModifyAtk(double multiplier) { }

}