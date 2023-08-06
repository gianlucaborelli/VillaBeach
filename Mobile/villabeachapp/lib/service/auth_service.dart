import 'package:firebase_auth/firebase_auth.dart';
import 'package:get/get.dart';

class AuthService extends GetxController {
  final FirebaseAuth _auth = FirebaseAuth.instance;
  Rxn<User?> _firebaseUser = Rxn<User?>();
  var userIsAuthenticated = false.obs;

  @override
  void onInit() {
    super.onInit();

    _firebaseUser = Rxn<User?>(_auth.currentUser);
    _firebaseUser.bindStream(_auth.authStateChanges());

    setupAuthListener();

    ever(_firebaseUser, (User? user) {
      if (user != null) {
        userIsAuthenticated.value = true;
      } else {
        userIsAuthenticated.value = false;
      }
    });
  }

  void setupAuthListener() {
    FirebaseAuth.instance.authStateChanges().listen(
      (User? user) {
        if (user == null) {
          userIsAuthenticated.value = false;
        }
      },
    );
  }

  User? get user => _firebaseUser.value;
  static AuthService get to => Get.find<AuthService>();

  // showSnack(String title, String error) {
  //   Get.showSnackbar(
  //     GetSnackBar(
  //       title: title,
  //       message: error,
  //       snackPosition: SnackPosition.BOTTOM,
  //       duration: const Duration(seconds: 3),
  //     ),
  //   );
  // }

  createUser(String email, String password) async {
    try {
      await _auth.createUserWithEmailAndPassword(
        email: email,
        password: password,
      );
      sendEmailVerification();
    } on FirebaseAuthException catch (e, s) {
      _handleFirebaseLoginWithCredentialsException(e, s);
    }
  }

  login(String email, String password) async {
    try {
      await _auth.signInWithEmailAndPassword(
        email: email,
        password: password,
      );
    } on FirebaseAuthException catch (e, s) {
      _handleFirebaseLoginWithCredentialsException(e, s);
    }
  }

  logout() async {
    try {
      await _auth.signOut();
    } on FirebaseAuthException catch (e, s) {
      _handleFirebaseLoginWithCredentialsException(e, s);
    }
  }

  Future<void> sendEmailVerification() async {
    try {
      _auth.currentUser?.sendEmailVerification();
    } catch (e) {
      //print('error');
    }
  }

  void _handleFirebaseLoginWithCredentialsException(
      FirebaseAuthException e, StackTrace s) {
    // if (e.code == 'user-disabled') {
    //   showSnack(
    //     'O usuário informado está desabilitado.',
    //     e.message.toString(),
    //   );
    // } else if (e.code == 'user-not-found') {
    //   showSnack(
    //     'O usuário informado não está cadastrado.',
    //     e.message.toString(),
    //   );
    // } else if (e.code == 'email-already-in-use') {
    //   showSnack(
    //     'O email informado já esta em uso.',
    //     e.message.toString(),
    //   );
    // } else if (e.code == 'invalid-email') {
    //   showSnack(
    //     'O domínio do e-mail informado é inválido.',
    //     e.message.toString(),
    //   );
    // } else if (e.code == 'wrong-password') {
    //   showSnack(
    //     'A senha informada está incorreta.',
    //     e.message.toString(),
    //   );
    // } else if (e.code == 'weak-password') {
    //   // showSnack(
    //   //   'A senha deve conter ao menos 6 caracteres.',
    //   //   e.message.toString(),
    //   );
    // } else {
    //   //showSnack('', e.message.toString());
    // }
  }
}
