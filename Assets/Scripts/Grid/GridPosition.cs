
public struct GridPosition
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x; 
        this.z = z;
    }

    // ToString method for our struct
    public override string ToString()
    {
        return (x + ", " + z);
    }

}