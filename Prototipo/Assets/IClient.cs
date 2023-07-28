using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Interface a ser implementada pelo "usuário da API"

public interface IClient {
    void handle(string ms);
}