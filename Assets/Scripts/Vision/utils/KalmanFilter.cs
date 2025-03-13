using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalmanFilter
{
    public float[] Filter(float[] z)
    {
        float[] xhat = new float[z.Length];
        xhat[0] = z[0];
        float P = 1;

        for (int k = 1; k < xhat.Length; k++)
        {
            float xhatminus = xhat[k - 1];
            float Pminus = P + Q;
            float K = Pminus / (Pminus + R);
            xhat[k] = xhatminus + K * (z[k] - xhatminus);
            P = (1 - K) * Pminus;
        }
        return xhat;
    }

    bool isFirst = true;
    bool haveSetFirst = false;
    float xhat;
    float P;
    float z0;
    float Q = 1e-5f; // 0.00001
    float R = 0.0001f;

    public void SetFirst(float z0)
    {
        this.z0 = z0;
        haveSetFirst = true;
    }

    // ÀíÏëÕ`²î
    public void SetQ(float Q)
    {
        this.Q = Q;
    }

    // ŒëHÕ`²î
    public void SetR(float R)
    {
        this.R = R;
    }

    public float Filter(float z1)
    {
        if (isFirst)
        {
            isFirst = false;
            if (haveSetFirst == false) z0 = z1;
            xhat = z0;
            P = 1;
        }

        float xhatminus = xhat;
        float Pminus = P + Q;
        float K = Pminus / (Pminus + R);
        xhat = xhatminus + K * (z1 - xhatminus);
        P = (1 - K) * Pminus;

        return xhat;
    }

    public void Reset()
    {
        isFirst = true;
        haveSetFirst = false;
        xhat = 0;
        P = 0;
        z0 = 0;
        Q = 1e-5f; // 0.00001
        R = 0.0001f;
    }
}

