﻿using System;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

internal class BobRunner : IDisposable
{
   private readonly ILogger _logger;
   private readonly ECDsa _signAlgorithm;

   public BobRunner(ILogger<AliceRunner> logger)
   {
      _logger = logger;
      _signAlgorithm = ECDsa.Create();
   }

   public void Dispose() => _signAlgorithm.Dispose();

   public byte[] GetPublicKey() => _signAlgorithm.ExportSubjectPublicKeyInfo();

   public void VerifySignature(byte[] data, byte[] signature, byte[] pubKey)
   {
      _signAlgorithm.ImportSubjectPublicKeyInfo(pubKey.AsSpan(), out var bytesRead);
      var success = _signAlgorithm.VerifyData(data, signature, HashAlgorithmName.SHA512);

      _logger.LogInformation($"Signature is ok: {success}");
   }
}