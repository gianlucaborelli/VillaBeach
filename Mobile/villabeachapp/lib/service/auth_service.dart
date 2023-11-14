import 'package:firebase_auth/firebase_auth.dart';
import 'package:get/get.dart';
import 'package:villabeachapp/widgets/snack_firebase_auth.dart';

class AuthService extends GetxController {
  final FirebaseAuth _auth = FirebaseAuth.instance;
  Rxn<User?> _firebaseUser = Rxn<User?>();
  var userIsAuthenticated = false.obs;

  @override
  void onInit() {
    super.onInit();

    _firebaseUser = Rxn<User?>(_auth.currentUser);
    _firebaseUser.bindStream(_auth.authStateChanges());

    ever(
      _firebaseUser,
      (User? user) {
        if (user != null) {
          userIsAuthenticated.value = true;
        } else {
          userIsAuthenticated.value = false;
        }
      },
    );
  }

  User? get user => _firebaseUser.value;
  static AuthService get to => Get.find<AuthService>();

  createUser(String email, String password, String name) async {
    try {
      var credential = await _auth.createUserWithEmailAndPassword(
        email: email,
        password: password,
      );

      credential.user?.updateDisplayName(name);

      sendEmailVerification();
    } on FirebaseAuthException catch (e) {
      SnackAuthError().show(e);
    }
  }

  login(String email, String password) async {
    try {
      await _auth.signInWithEmailAndPassword(
        email: email,
        password: password,
      );
    } on FirebaseAuthException catch (e) {
      SnackAuthError().show(e);
    }
  }

  resetPassword(String email) async {
    try {
      await _auth.sendPasswordResetEmail(
        email: email,
      );
    } on FirebaseAuthException catch (e) {
      SnackAuthError().show(e);
    }
  }

  logout() async {
    try {
      await _auth.signOut();
    } on FirebaseAuthException catch (e) {
      SnackAuthError().show(e);
    }
  }

  Future<void> sendEmailVerification() async {
    try {
      _auth.currentUser?.sendEmailVerification();
    } on FirebaseAuthException catch (e) {
      SnackAuthError().show(e);
    }
  }
}
