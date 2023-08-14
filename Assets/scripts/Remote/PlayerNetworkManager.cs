using Unity.Netcode;
using UnityEngine;

public class PlayerNetworkManager : NetworkBehaviour
{

    public static ulong ObjectId;

    public override void OnNetworkSpawn()
    {
        ObjectId = NetworkObjectId;

        if (!IsServer && IsOwner)
        {
            MovementPacketServerRpc(this.gameObject.transform.position, this.gameObject.transform.rotation, NetworkObjectId);
        }
    }

    public void SendMovementUpdate(Vector3 position, Quaternion rotation)
    {
        // MovementPacketServerRpc(position, rotation, NetworkObjectId);
    }

    [ClientRpc]
    /// A key input (a network packet that sends a position is a Absoloute network packet)
    void MovementPacketClientRpc(Vector3 position, Quaternion rotation, ulong sourceNetworkObjectId)
    {

        this.gameObject.transform.position = position;
        this.gameObject.transform.rotation = rotation;

    }

    [ServerRpc]
    /// A key input (a network packet that sends a position is a Absoloute network packet) (towards a server)
    void MovementPacketServerRpc(Vector3 position, Quaternion rotation , ulong sourceNetworkObjectId)
    {

        this.gameObject.transform.position = position;
        this.gameObject.transform.rotation = rotation;

    }
}

