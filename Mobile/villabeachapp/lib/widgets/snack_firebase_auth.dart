import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class SnackAuthError {
  show(FirebaseAuthException exception) {
    return Get.showSnackbar(
      GetSnackBar(
        icon: const Icon(
          Icons.error_outline_outlined,
          color: Colors.white70,
        ),
        title: 'Erro de Autenticação',
        message: _handleFirebaseLoginWithCredentialsException(exception),
        backgroundColor: Colors.black87,
        duration: const Duration(seconds: 3),
      ),
    );
  }

  String? _handleFirebaseLoginWithCredentialsException(
      FirebaseAuthException exception) {
    switch (exception.code) {
      case 'user-disabled':
        return 'O usuário informado está desabilitado.';
      case 'user-not-found':
        return 'O usuário informado não está cadastrado.';
      case 'email-already-in-use':
        return 'O email informado ja esta em uso.';
      case 'invalid-email':
        return 'O domínio do e-mail informado é inválido.';
      case 'wrong-password':
        return 'A senha ou e-mail informado está incorreta.';
      case 'weak-password':
        return 'A senha deve conter ao menos 6 caracteres.';
      default:
        return exception.message.toString();
    }
  }
}
