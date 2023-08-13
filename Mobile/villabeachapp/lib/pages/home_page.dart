import 'package:flutter/material.dart';
import 'package:villabeachapp/widgets/appbar_custom.dart';
import 'package:villabeachapp/widgets/navbar.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      drawer: NavBar(),
      appBar: AppBarCustom(),
    );
  }
}
