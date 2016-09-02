// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace fulcrum_api.DependencyResolution {
    using Controllers.Login;
    using Controllers.MessageBoard;
    using Controllers.Users;
    using fulcrum_services.Services;
    using fulcrum_services.Services.IdentityOwin;
    using StructureMap;

    public class DefaultRegistry : Registry {

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.AssemblyContainingType<IGenericService>();
                    scan.WithDefaultConventions();
                });

            //Register Repositories Below

            //Register Services Below

            //Register Controllers Below
            ForConcreteType<LoginController>()
                .Configure.Ctor<IOwinService>().Is<OwinService>();

            ForConcreteType<UserController>()
                .Configure.Ctor<IGenericService>().Is<GenericService>();

            ForConcreteType<MessageBoardController>()
                .Configure.Ctor<IGenericService>().Is<GenericService>();
        }
    }
}