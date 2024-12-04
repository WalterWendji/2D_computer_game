using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Managers;

public static class ResourceManager
{
    public static Texture2D enCRunTexture;
    public static Texture2D enCAttackTexture;
    public static Texture2D enCDamageTexture;
    public static Texture2D enCDeathTexture;

    public static Texture2D enNRunTexture;
    public static Texture2D enNAttackTexture;
    public static Texture2D enNDamageTexture;
    public static Texture2D enNDeathTexture;

    public static Texture2D enTWalkTexture;
    public static Texture2D enTAttackTexture;
    public static Texture2D enTDamageTexture;
    public static Texture2D enTDeathTexture;
    public static Texture2D enTBlockTexture;

    public static Texture2D enFWalkTexture;
    public static Texture2D enFDamageTexture;
    public static Texture2D enFDeathTexture;
    public static Texture2D enFIdleTexture;
    public static Texture2D enFIdle_2Texture;
    public static Texture2D enFAttack_1Texture;
    public static Texture2D enFShot_1Texture;
    public static Texture2D enFShot_2Texture;

    public static Texture2D runTexture;
    public static Texture2D idleTexture;
    public static Texture2D jumpTexture;
    public static Texture2D attackTexture;
    public static Texture2D deathTexture;
    public static Texture2D damageTexture;

    public static Texture2D shIdleTexture;
    public static Texture2D shDialogueTexture;
    public static Texture2D shGreetingTexture;
    public static Texture2D shApprovalTexture;

    public static void LoadContent(ContentManager content)
    {
        enCRunTexture = content.Load<Texture2D>("Player_Level1/Warrior_2/Run");
        enCAttackTexture = content.Load<Texture2D>("Player_Level1/Warrior_2/Run+Attack");
        enCDamageTexture = content.Load<Texture2D>("Player_Level1/Warrior_2/Hurt");
        enCDeathTexture = content.Load<Texture2D>("Player_Level1/Warrior_2/Dead");

        enNRunTexture = content.Load<Texture2D>("Player_Level1/EnemyN/enRun");
        enNAttackTexture = content.Load<Texture2D>("Player_Level1/EnemyN/enRun+Attack");
        enNDamageTexture = content.Load<Texture2D>("Player_Level1/EnemyN/enHurt");
        enNDeathTexture = content.Load<Texture2D>("Player_Level1/EnemyN/enDead");

        enTWalkTexture = content.Load<Texture2D>("Player_Level1/Warrior_3/Walk");
        enTAttackTexture = content.Load<Texture2D>("Player_Level1/Warrior_3/Attack_1");
        enTDamageTexture = content.Load<Texture2D>("Player_Level1/Warrior_3/Hurt");
        enTDeathTexture = content.Load<Texture2D>("Player_Level1/Warrior_3/Dead");
        enTBlockTexture = content.Load<Texture2D>("Player_Level1/Warrior_3/Protect");

        enFWalkTexture = content.Load<Texture2D>("Player_Level1/EnemyF/Walk");
        enFAttack_1Texture = content.Load<Texture2D>("Player_Level1/EnemyF/Attack_1");
        enFDamageTexture = content.Load<Texture2D>("Player_Level1/EnemyF/Hurt");
        enFDeathTexture = content.Load<Texture2D>("Player_Level1/EnemyF/Dead");
        enFIdleTexture = content.Load<Texture2D>("Player_Level1/EnemyF/Idle");
        enFIdle_2Texture = content.Load<Texture2D>("Player_Level1/EnemyF/Idle_2");
        enFShot_1Texture = content.Load<Texture2D>("Player_Level1/EnemyF/Shot_1");
        enFShot_2Texture = content.Load<Texture2D>("Player_Level1/EnemyF/Shot_2");

        runTexture = content.Load<Texture2D>("Player_Level1/Warrior_1/Run");
        idleTexture = content.Load<Texture2D>("Player_Level1/Warrior_1/Idle");
        jumpTexture = content.Load<Texture2D>("Player_Level1/Warrior_1/Jump2");
        attackTexture = content.Load<Texture2D>("Player_Level1/Warrior_1/Attack_1");
        deathTexture = content.Load<Texture2D>("Player_Level1/Warrior_1/Dead");
        damageTexture = content.Load<Texture2D>("Player_Level1/Warrior_1/Hurt");

        shIdleTexture = content.Load<Texture2D>("NPCs/Shopkeeper/Idle");
        shDialogueTexture = content.Load<Texture2D>("NPCs/Shopkeeper/Dialogue");
        shGreetingTexture = content.Load<Texture2D>("NPCs/Shopkeeper/Idle_2");
        shApprovalTexture = content.Load<Texture2D>("NPCs/Shopkeeper/Approval");
    }
}
