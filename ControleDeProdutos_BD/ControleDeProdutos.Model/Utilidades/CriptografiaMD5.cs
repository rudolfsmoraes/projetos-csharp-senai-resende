﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

public static class CriptografiaMD5
{
    public static string CriptografarSenha(string senha)
    {
        MD5 md5Hash = MD5.Create();

        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

        StringBuilder sBuilder = new StringBuilder();

        for(int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();
    }
}
