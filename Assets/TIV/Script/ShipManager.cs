public static class ShipManager
{
    public static UserHaveShipData CreateShip(int shipTableKey)
    {
        UserHaveShipData shipData = new UserHaveShipData(shipTableKey);
        JsonDataManager.jsonCache.UserHaveShipDataListCache.list[shipTableKey]._isHave = true;
        return new UserHaveShipData(shipTableKey);
    }
}
