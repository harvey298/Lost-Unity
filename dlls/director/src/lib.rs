use rand::Rng;

enum EnemyTypes {
    Goblin = 10,
    Elf = 20,
    Orc = 30,
    Troll = 40,
    Zombie = 50,
    Skeleton = 60,
    Dragon = 70,
    Phantom = 80,
    PhantomGoblin = 90,
    PhantomElf = 100,
    PhantomOrc = 110,
    PhantomTroll = 120,
    PhantomZombie = 130,
    PhantomSkeleton = 140,
    PhantomDragon = 150,
    PhantomPhantom = 160,
}


pub extern "C" fn buy_enemy(credits: i64) -> BoughtEnemy {
    let mut credits = credits;
    // Pick an enemy randomly
    let mut rng = rand::thread_rng();
    let enemy_type = rng.gen_range(1..=1000);
    




    todo!()
}

#[repr(C)]
pub struct BoughtEnemy {
    pub new_credits: i64,
    pub enemy_id: usize
}

impl EnemyTypes {

    pub fn to_str(&self) -> String {
        match self {
            EnemyTypes::Goblin => "Goblin",
            EnemyTypes::Elf => "Elf",
            EnemyTypes::Orc => "Orc",
            EnemyTypes::Troll => "Troll",
            EnemyTypes::Zombie => "Zombie",
            EnemyTypes::Skeleton => "Skeleton",
            EnemyTypes::Dragon => "Dragon",
            EnemyTypes::Phantom => "Phantom",
            EnemyTypes::PhantomGoblin => "Phantom Goblin",
            EnemyTypes::PhantomElf => "Phantom Elf",
            EnemyTypes::PhantomOrc => "Phantom Orc",
            EnemyTypes::PhantomTroll => "Phantom Troll",
            EnemyTypes::PhantomZombie => "Phantom Zombie",
            EnemyTypes::PhantomSkeleton => "Phantom Skeleton",
            EnemyTypes::PhantomDragon => "Phantom Dragon",
            EnemyTypes::PhantomPhantom => "Phantom Phantom",
        }.to_string()
    }


}