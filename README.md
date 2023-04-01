<div align=Center>
<h1>:sunrise: VillaBeach Project - Web Service</h1>
</div>

<img align="right" height="400em" src="https://github.com/gianlucaborelli/VillaBeach-WebService/blob/cd5a93ba24ef494676f7b8106b03211dd4824bb3/img/DALL%C2%B7E%202023-03-03%2001.16.29%20-%20on%20expressive%20oil%20painting%20voley%20beach%20.png" alt="Voley Beach, OpenIa image">

<p align="justify">Projeto desenvolvido em substituição ao <a target="_blank" href="https://github.com/gianlucaborelli/ProjectVillaBeach-DEPRECATED-">projeto Winform</a> , que teve seu desenvolvimento descontinuado, buscando melhor atender as necessidades do cliente, como alta disponibilidade e multiplataforma.</p>

## About this Project 

<p align="justify"> This project aims to assist in the management of a company called <b>VillaBeach</b> located in the city of <a target="_blank" href="https://www.google.com/maps/place/Brodowski,+SP,+14340-000/@-20.9915545,-47.6566512,14">Brodowski</a>, in the countryside of the State of São Paulo, and its core business is a gym environment with a beach volleyball court joined together with a pub and recreation environment. </p>

<p align="justify"> With the full consent of the company's owners, this project will not have the purpose of selling and monetization, being fully developed to practice my knowledge and compose my portfolio. </p>

<p align="justify"> <b>OBS:</b> All third-party material used in this project are included in the "References" tab at the end of this page, as well as included in the source code when applicable. </p>

## Why? 

The development of this project is carried out during my free time and during my studies, and was conceived after understanding the needs of a co-worker regarding the difficulties he has been facing in controlling the finances of his private bussiness. 

This project is part of my personal portfolio, so, I'll be happy if you could provide me any feedback about the project, code, structure or anything that you can report that could make me a better developer! 

Also, you can use this Project as you wish, be for study, be for make improvements or earn money with it! 

It's free! 

The only request when using this project is to reference this repository, the citation/reference when validly occurs provides respect for this work and the authors of the contents used as a reference.

###  Cenário atual

Para cada nova pessoa matriculada na academia, é oferecida uma avaliação física, o qual deve ser feito o agendamento do dia e horário a partir de um formulário disponibilizado via [Google Form](https://docs.google.com/forms/).

Ao realizar a avaliação física, o avaliador anota as informações obtidas em um relatório impresso genérico guardando uma via para a academia e outra para o cliente.

O controle de pagamentos das mensalidades da academia e o controle das vendas e estoque do bar são feitos a partir de uma planilha de Excel.

### As queixas

A principal queixa do meu cliente é em relação a visualização de relatórios de vendas e de pagamentos de mensalidades, que atualmente deve ser feito manualmente todos os meses, bem como uma maneira mais intuitiva de registrar essas ações e que não fosse limitado ao computador onde se localizava o arquivo Excel.

Outra queixa é em relação ao agendamento das avaliações físicas. A utilização do Google Forms não possibilita impor limites dos horários de atendimentos, causando conflitos de horários, agendamentos fora do horário de atendimento, além de que, para visualizar cada agendamento, deve-se acessar a plataforma google e gerar um novo relatório do google Forms.

Sobre as avaliações físicas, a principal queixa é a impossibilidade de fornecer facilmente uma visualização da evolução física dos clientes da empresa, como também o fato de ter que armazenar fisicamente os relatórios de cada cliente. 

### Proposta

De maneira inexperiente das novas tecnologias, inicialmente propus realizar um aplicativo utilizando [Windows Forms com banco de dados local](https://github.com/gianlucaborelli/ProjectVillaBeach-DEPRECATED-) . Porém percebi que essa abordagem não solucionaria uma das necessidades dos clientes que é a alta disponibilidade, uma vez que um dos proprietários trabalha em outra cidade e gostaria de acompanhar as informações da academia em tempo real de onde estiver. 

Com isso em mente, propus reiniciar o projeto, dessa vez disponibilizando as principais funcionalidades de forma online através de um Web Service, e como forma de acessar essas funcionalidades, propus a criação de um Aplicativo Mobile e um Web Site com utilização tanto dos clientes da academia, como também para uso administrativo pelos proprietários.

Para solucionar o problema dos agendamentos de horários da avaliação física propus a utilização do serviço chamado [Calendly](https://calendly.com/), que desde os recursos gratuitos disponibilizados soluciona as necessidades referente aos agendamentos, além de disponibilizar também:

* Integração com agendas dos celulares como Calendário do Google e Outlook .
* Facilidade na integração a Sites.
* APIs de integrações.


## Some Observations about this App 
