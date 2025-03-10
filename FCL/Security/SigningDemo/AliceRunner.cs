﻿using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

internal class AliceRunner : IDisposable
{
   private readonly ILogger _logger;
   private readonly ECDsa _signAlgorithm;

   public AliceRunner(ILogger<AliceRunner> logger)
   {
      _logger = logger;
      _signAlgorithm = ECDsa.Create();
      _logger.LogInformation($"Using this ECDsa class: {_signAlgorithm.GetType().Name}");
   }

   public void Dispose() => _signAlgorithm.Dispose();

   public byte[] GetPublicKey() => _signAlgorithm.ExportSubjectPublicKeyInfo();

   public (byte[] Data, byte[] Sign) GetDocumentAndSignature()
   {
      byte[] aliceData = Encoding.UTF8.GetBytes("I'm Alice");
      byte[] aliceDataSignature = _signAlgorithm.SignData(aliceData, HashAlgorithmName.SHA512);
      return (aliceData, aliceDataSignature);
   }
}