class Galletas: 
    def __init__(self,sabor):
        self.sabor = sabor

    def sabor_galleta(self):
     return f"El sabor de la galleta es de {self.sabor}"
s=Galletas("chocolate")
print(s.sabor_galleta())
