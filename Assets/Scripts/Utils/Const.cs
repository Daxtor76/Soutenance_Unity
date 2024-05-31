public static class Const
{
    // Values
    public const float GRAVITY = -9.81f;
    public const float CHARACTER_JUMP_HEIGHT = 5.0f;
    public const float CHARACTER_FORWARD_SPEED = 3.0f;
    public const float CHARACTER_SNEAKY_FORWARD_SPEED = 1f;
    public const float CHARACTER_STRAFE_SPEED = 3.0f;
    public const float CHARACTER_SNEAKY_STRAFE_SPEED = 1.0f;
    public const float CHARACTER_ROTATION_SPEED = 40.0f;
    public const float ENEMY_FORWARD_SPEED = 2.0f;
    public const float ENEMY_SIDE_SPEED = 3.0f;
    
    // Tags
    public const string OBSTACLE_TAG_NAME = "Obstacle";
    public const string PATOUNE_TAG_NAME = "Patoune";
    public const string TILESPAWNER_TAG_NAME = "TileSpawner";
    public const string LEFT_ROTATOR_TAG_NAME = "LeftRotator";
    public const string RIGHT_ROTATOR_TAG_NAME = "RightRotator";
    public const string ENEMY_TAG_NAME = "Enemy";
    public const string CHARACTER_TAG_NAME = "Character";
    
    // Controls
    public const string STRAFE_AXIS_NAME = "Strafe";
    public const string RUN_AXIS_NAME = "Run";
    public const string JUMP_AXIS_NAME = "Jump";
    public const string SNEAK_AXIS_NAME = "Sneak";

    // Paths & names
    public const string PATH_TO_CHARACTER_FOLDER = "Prefabs/Characters/";
    public const string CHARACTER_NAME = "Character";
    public const string PATH_TO_CAMERA_FOLDER = "Prefabs/Camera/";
    public const string CAMERA_NAME = "Camera";
    public const string PATH_TO_TILES_FOLDER = "Prefabs/Tiles/";
    public const string INITIAL_TILE_NAME = "Tile_Beginning";
    public const string COMMON_TILE_NAME = "Tile";
    public const string PATH_TO_MANAGERS_FOLDER = "Prefabs/Managers/";
    public const string LEVEL_MANAGER_NAME = "LevelManager";

    public const string CAMERA_TARGET_DUMMY = "CameraTargetDummy";
    public const string CAMERA_NORMAL_DUMMY = "CameraNormalDummy";
    public const string CAMERA_KYUBI_DUMMY = "CameraKyubiDummy";
    public const string TILE_NEXT_SPAWN_DUMMY = "NextSpawnDummy";
    public const string TILE_PATOUNES_DUMMY = "Patounes";
    
    // UI
    public const string SCORING_TEXT_NAME = "ScoringText";

    // LayerMasks
    public const string LAYERMASK_GROUND_NAME = "Ground";
}