#--------------------------------------------------------------------------------------
# Script para Generar un Certificado Autofirmado Exportable y Exportarlo a Formato PFX
#--------------------------------------------------------------------------------------

# 1. Definir variables
$SubjectName = "CN=CERT-UAE"
$FriendlyName = "Certificado UAE"
$PfxFilePath = "C:\Users\jhonatan.moreno\Documents\repos\CertUAE\CertUAE\CERT-UAE.pfx"
$PasswordString = "Q&(w3Gf,iZME+Lx0"

# 2. Crear un certificado autofirmado con Exportable = $true
Write-Host "Creando el certificado autofirmado '$SubjectName' con clave exportable..."
try {
    $cert = New-SelfSignedCertificate -Subject $SubjectName `
                                    -CertStoreLocation "Cert:\CurrentUser\My" `
                                    -KeyAlgorithm RSA `
                                    -KeyLength 2048 `
                                    -NotBefore (Get-Date) `
                                    -NotAfter (Get-Date).AddYears(1) `
                                    -KeySpec Signature `
                                    -KeyUsage DigitalSignature `
                                    -FriendlyName $FriendlyName `
                                    -HashAlgorithm SHA256 `
                                    -KeyExportPolicy Exportable `
                                    -ErrorAction Stop
    Write-Host "Certificado creado exitosamente. Thumbprint: $($cert.Thumbprint)"
}
catch {
    Write-Error "Error al crear el certificado: $($_.Exception.Message)"
    exit 1
}

# 3. Convertir la contraseña a un formato seguro
Write-Host "Preparando la contraseña para la exportación del PFX..."
try {
    $password = ConvertTo-SecureString -String $PasswordString -Force -AsPlainText -ErrorAction Stop
}
catch {
    Write-Error "Error al convertir la contraseña: $($_.Exception.Message)"
    exit 1
}

# 4. Exportar el certificado a un archivo PFX
Write-Host "Exportando el certificado a '$PfxFilePath'..."
try {
    Export-PfxCertificate -Cert $cert `
                        -FilePath $PfxFilePath `
                        -Password $password `
                        -ErrorAction Stop
    Write-Host "Certificado PFX exportado exitosamente a '$PfxFilePath'."
}
catch {
    Write-Error "Error al exportar el certificado PFX: $($_.Exception.Message)"
    exit 1
}

Write-Host "Proceso completado."