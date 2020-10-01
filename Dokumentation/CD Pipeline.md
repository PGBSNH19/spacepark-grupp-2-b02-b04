# Continuous Delivery Pipeline

Vi planerade att ha en Continuous Delivery Pipeline för deployment av hela projektet, vilket skulle göras utifrån ett Container Registry där vi hämtar respektiva Container images för Front och Backend, för att sedan deploya dessa till Container Instances.

Därefter fanns det en tanke på att implementera release ut till produktion med hjälp av ett QA(Quality Assurance) stadie, där tanken skulle vara att införa tester, manuella som automatiska integrationstest och liknande. Vi läste en artikel(https://shorturl.at/lFGOS) som hänvisar till hur man kan sätta upp en utvecklingsmiljö och en produktionsmiljö, där just QA dyker upp, som ett stadie för att säkerställa att produkten är hållbar och redo att leveras ut i produktion.



![](Flowchart Continuous Delivery planering.png) 

Vi insåg snart att det inte  skulle fungera på ett smidigt sätt, vilket resulterade i att vi valde att hantera det utifrån två Container Registrys och två Release-Pipelines, t.ex om vi endast har ändrat i våran Frontend, då ska inte API-delen pushas upp till sin Container Instance.

Tanken om att implementera en Produktionsmiljö blev att vi slopade, då vi insåg att det kom till att bli väldigt överflödigt då vi inte skapat integrationstester och liknande, samt att tiden inte riktigt fanns.

![](Continuous Delivery Pipeline Final Flowchart.png) 

Resultatet av våran CD Pipeline är att vi deployar våran Development miljö till separata Container Instances.